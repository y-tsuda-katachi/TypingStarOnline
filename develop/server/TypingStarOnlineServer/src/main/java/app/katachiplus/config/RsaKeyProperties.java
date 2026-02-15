package app.katachiplus.config;

import java.security.interfaces.RSAPrivateKey;
import java.security.interfaces.RSAPublicKey;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "rsa")
public class RsaKeyProperties {
	private RSAPublicKey publicKey;
	private RSAPrivateKey privateKey;

	public RSAPublicKey getPublicKey() {
		return publicKey;
	}

	public RSAPrivateKey getPrivateKey() {
		return privateKey;
	}

	public void setPublicKey(RSAPublicKey publicKey) {
		this.publicKey = publicKey;
	}

	public void setPrivateKey(RSAPrivateKey privateKey) {
		this.privateKey = privateKey;
	}
}
