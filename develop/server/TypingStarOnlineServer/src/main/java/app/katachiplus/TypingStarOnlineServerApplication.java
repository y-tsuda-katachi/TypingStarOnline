package app.katachiplus;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.EnableConfigurationProperties;

import app.katachiplus.config.RsaKeyProperties;

@SpringBootApplication
@EnableConfigurationProperties(RsaKeyProperties.class)
public class TypingStarOnlineServerApplication {

	public static void main(String[] args) {
		SpringApplication.run(TypingStarOnlineServerApplication.class, args);
	}
}
