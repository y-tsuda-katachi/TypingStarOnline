package app.katachiplus.domain.model.message.handler;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.AbstractWebSocketHandler;

import app.katachiplus.domain.service.PlayerService;

@Component
public class PlayerMessageHandler extends AbstractWebSocketHandler {
	
	@Autowired
	private PlayerService playerService;
	
	@Override
	public void afterConnectionEstablished(WebSocketSession session) throws Exception {
		var playerId = session.getHandshakeHeaders().getFirst("Player-Id");
		var password = session.getHandshakeHeaders().getFirst("Password");
		
	}
}
