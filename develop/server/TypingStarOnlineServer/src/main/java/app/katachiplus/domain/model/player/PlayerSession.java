package app.katachiplus.domain.model.player;

import org.springframework.web.socket.WebSocketSession;

import app.katachiplus.domain.model.match.MatchSession;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class PlayerSession {
	private String playerId;
	private WebSocketSession session;
	
	@Override
	public boolean equals(Object other) {
		if (other instanceof MatchSession)
			return this.playerId == ((PlayerSession) other).getPlayerId();
		return super.equals(other);
	}
	
	@Override
	public int hashCode() {
		return super.hashCode();
	}
}
