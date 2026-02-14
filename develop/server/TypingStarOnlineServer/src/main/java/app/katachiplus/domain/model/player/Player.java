package app.katachiplus.domain.model.player;

import app.katachiplus.domain.model.GameResult;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Player {
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
}