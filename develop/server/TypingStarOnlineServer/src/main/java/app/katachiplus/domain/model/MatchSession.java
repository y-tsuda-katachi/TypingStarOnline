package app.katachiplus.domain.model;

import org.springframework.web.socket.WebSocketSession;

import app.katachiplus.utility.KSet;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class MatchSession {
	private String matchId;
	private KSet<WebSocketSession> sessions;
	
	@Override
	public boolean equals(Object other) {
		if (other instanceof MatchSession)
			return this.matchId == ((MatchSession) other).getMatchId();
		return super.equals(other);
	}
	
	@Override
	public int hashCode() {
		return super.hashCode();
	}
}
