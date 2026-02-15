package app.katachiplus.domain.model.player;

import java.util.Collection;
import java.util.Collections;

import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;

import app.katachiplus.domain.model.GameResult;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Player implements UserDetails {
	private String id;
	private String password;
	private GameResult gameResult;
	private Long lastAccessedTime;

	public boolean hasGameResult() {
		return this.gameResult != null;
	}

	@Override
	public boolean equals(Object other) {
		if (other instanceof Player)
			return this.id == ((Player) other).getId();
		return super.equals(other);
	}

	@Override
	public int hashCode() {
		return super.hashCode();
	}

	@Override
	public Collection<? extends GrantedAuthority> getAuthorities() {
		// TODO 権限情報を持たせるか検討
		return Collections.emptyList();
	}

	@Override
	public String getUsername() {
		return this.id;
	}
}