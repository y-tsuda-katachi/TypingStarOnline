package app.katachiplus.domain.service;

import java.time.Instant;
import java.time.temporal.ChronoUnit;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.core.Authentication;
import org.springframework.security.oauth2.jwt.JwtClaimsSet;
import org.springframework.security.oauth2.jwt.JwtEncoder;
import org.springframework.security.oauth2.jwt.JwtEncoderParameters;
import org.springframework.stereotype.Service;

@Service
public class JwtTokenService {
	
	@Autowired
	private JwtEncoder jwtEncoder;
	
	public String generateToken(Authentication authentication) {
		var now = Instant.now();
		var claims = JwtClaimsSet.builder()
				.issuer("oauth2")
				.issuedAt(now)
				.expiresAt(now.plus(30, ChronoUnit.MINUTES))
				.subject("x-auth-token")
				.claim("name", authentication.getName())
				.build();
		return jwtEncoder.encode(JwtEncoderParameters.from(claims)).getTokenValue();
	}
}
