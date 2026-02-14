package app.katachiplus.domain.model.message.handler;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.AbstractWebSocketHandler;

import app.katachiplus.domain.model.player.PlayerSession;
import app.katachiplus.domain.service.PlayerService;
import app.katachiplus.utility.KSet;

@Component
public class PlayerMessageHandler extends AbstractWebSocketHandler {

	private final KSet<PlayerSession> playerSessions = new KSet<>();
	@Autowired
	private PlayerService playerService;

	@Override
	public void afterConnectionEstablished(WebSocketSession session) throws Exception {
		var playerId = session.getHandshakeHeaders().getFirst("Player-Id");
		playerSessions.add(new PlayerSession(playerId, session));
	}

	@Override
	public void afterConnectionClosed(WebSocketSession session, CloseStatus status) throws Exception {
		var playerId = session.getHandshakeHeaders().getFirst("Player-Id");
		var playerSession = playerSessions.selectOne(ps -> ps.getPlayerId().equals(playerId));
		playerSessions.remove(playerSession);
		// コネクションが失われたら自動的にログアウトする
		playerService.logout(playerId);
	}
}
