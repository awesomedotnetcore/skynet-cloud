package cn.uway.skynet.cloud.auth.config;

import cn.uway.skynet.cloud.auth.dto.SkynetCloudJwtAccessTokenConvertor;
import cn.uway.skynet.cloud.common.core.constant.CommonConstants;
import cn.uway.skynet.cloud.common.core.constant.SecurityConstants;
import cn.uway.skynet.cloud.common.security.component.SkynetCloudWebResponseExceptionTranslator;
import cn.uway.skynet.cloud.common.security.service.SkynetCloudClientDetailsServiceImpl;
import cn.uway.skynet.cloud.common.security.service.UserDetailIpml;
import lombok.AllArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.redis.connection.RedisConnectionFactory;
import org.springframework.http.HttpMethod;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.oauth2.common.DefaultOAuth2AccessToken;
import org.springframework.security.oauth2.config.annotation.configurers.ClientDetailsServiceConfigurer;
import org.springframework.security.oauth2.config.annotation.web.configuration.AuthorizationServerConfigurerAdapter;
import org.springframework.security.oauth2.config.annotation.web.configuration.EnableAuthorizationServer;
import org.springframework.security.oauth2.config.annotation.web.configurers.AuthorizationServerEndpointsConfigurer;
import org.springframework.security.oauth2.config.annotation.web.configurers.AuthorizationServerSecurityConfigurer;
import org.springframework.security.oauth2.provider.token.TokenEnhancer;
import org.springframework.security.oauth2.provider.token.TokenEnhancerChain;
import org.springframework.security.oauth2.provider.token.TokenStore;
import org.springframework.security.oauth2.provider.token.store.JwtAccessTokenConverter;
import org.springframework.security.oauth2.provider.token.store.redis.RedisTokenStore;
import javax.sql.DataSource;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;

@Configuration
//@AllArgsConstructor
@EnableAuthorizationServer
public class AuthorizationServerConfig extends AuthorizationServerConfigurerAdapter {
    private final DataSource dataSource;
    private final UserDetailsService userDetailsService;
    private final AuthenticationManager authenticationManager;
    private final RedisConnectionFactory redisConnectionFactory;

    public AuthorizationServerConfig(DataSource dataSource, UserDetailsService userDetailsService, AuthenticationManager authenticationManager, RedisConnectionFactory redisConnectionFactory) {
        this.dataSource = dataSource;
        this.userDetailsService = userDetailsService;
        this.authenticationManager = authenticationManager;
        this.redisConnectionFactory = redisConnectionFactory;
    }

    @Override
    public void configure(ClientDetailsServiceConfigurer clients) throws Exception {
        SkynetCloudClientDetailsServiceImpl clientDetailsService = new SkynetCloudClientDetailsServiceImpl(dataSource);
        clientDetailsService.setSelectClientDetailsSql(SecurityConstants.DEFAULT_SELECT_STATEMENT);
        clientDetailsService.setFindClientDetailsSql(SecurityConstants.DEFAULT_FIND_STATEMENT);
        clients.withClientDetails(clientDetailsService);
    }

    @Override
    public void configure(AuthorizationServerSecurityConfigurer oauthServer) {
        oauthServer
                .allowFormAuthenticationForClients()
                .checkTokenAccess("permitAll()");
    }

    @Override
    public void configure(AuthorizationServerEndpointsConfigurer endpoints) {
        TokenEnhancerChain tokenEnhancerChain = new TokenEnhancerChain();
        tokenEnhancerChain.setTokenEnhancers(Arrays.asList(tokenEnhancer(), jwtAccessTokenConverter()));
        endpoints
                .allowedTokenEndpointRequestMethods(HttpMethod.GET, HttpMethod.POST)

                .tokenStore(tokenStore())
                .tokenEnhancer(tokenEnhancerChain)
//                .tokenEnhancer(tokenEnhancer())
//                .accessTokenConverter(jwtAccessTokenConverter())
                .userDetailsService(userDetailsService)
                .authenticationManager(authenticationManager)
                .reuseRefreshTokens(false)
                .exceptionTranslator(new SkynetCloudWebResponseExceptionTranslator());
    }

    @Bean
    public JwtAccessTokenConverter jwtAccessTokenConverter() {
        SkynetCloudJwtAccessTokenConvertor jwtAccessTokenConvertor = new SkynetCloudJwtAccessTokenConvertor();
        jwtAccessTokenConvertor.setSigningKey(CommonConstants.SIGN_KEY);
        return jwtAccessTokenConvertor;
    }

    @Bean
    public TokenStore tokenStore() {
        RedisTokenStore tokenStore = new RedisTokenStore(redisConnectionFactory);
        tokenStore.setPrefix(SecurityConstants.PROJECT_PREFIX + SecurityConstants.OAUTH_PREFIX);
        return tokenStore;
    }

    @Bean
    public TokenEnhancer tokenEnhancer() {
        return (accessToken, authentication) -> {
            final Map<String, Object> additionalInfo = new HashMap<>(1);
            additionalInfo.put("license", SecurityConstants.PROJECT_LICENSE);
            UserDetailIpml user = (UserDetailIpml)authentication.getUserAuthentication().getPrincipal();
            additionalInfo.put("id", user.getId());
            additionalInfo.put("org", user.getDeptId());
            ((DefaultOAuth2AccessToken) accessToken).setAdditionalInformation(additionalInfo);
            return accessToken;
        };
    }
}
