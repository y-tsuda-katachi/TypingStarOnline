package app.katachiplus.domain.model;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Player implements Comparable<Player> {
	private String id;
	private String name;
	private GameResult gameResult;
	private Long lastAccessedTime;

	public Player(String id, String name, Long lastAccessedTime) {
		this(id, name, null, lastAccessedTime);
	}

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
	public int compareTo(Player other) {
		return Float.compare(
				this.getGameResult().getElapsedMilliSeconds(),
				((Player) other).getGameResult().getElapsedMilliSeconds());
	}
}