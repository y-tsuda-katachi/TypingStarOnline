package app.katachiplus.domain.model;

import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Player implements Comparable<Player> {
	private String playerId;
	private String playerName;
	private SseEmitter emitter;
	private GameResult gameResult;
	private PlayerState state;
	private Long lastAccessedTime;

	public Player(String playerId, String playerName, Long lastAccessedTime) {
		this(
				playerId,
				playerName,
				null,
				null,
				PlayerState.Connected,
				lastAccessedTime);
	}

	public boolean hasEmitter() {
		return this.emitter != null;
	}

	public boolean hasGameResult() {
		return this.gameResult != null;
	}

	@Override
	public boolean equals(Object other) {
		if (this == other)
			return true;
		if (other instanceof Player)
			return this.playerId == ((Player) other).getPlayerId();
		return false;
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