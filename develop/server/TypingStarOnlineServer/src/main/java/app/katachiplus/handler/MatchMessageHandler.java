package app.katachiplus.handler;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.WebSocketMessage;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.AbstractWebSocketHandler;

import app.katachiplus.domain.model.MatchSession;
import app.katachiplus.domain.model.message.MatchMessage;
import app.katachiplus.domain.service.MatchService;
import app.katachiplus.domain.service.PlayerService;
import app.katachiplus.utility.KSet;

@Component
public class MatchMessageHandler extends AbstractWebSocketHandler {

	private final KSet<MatchSession> matchSessions = new KSet<>();
	@Autowired
	private PlayerService playerService;
	@Autowired
	private MatchService matchService;

	@Override
	public void afterConnectionEstablished(WebSocketSession session) throws Exception {
		var matchId = session.getHandshakeHeaders().getFirst("Match-Id");
		var matchSession = matchSessions.selectOne(ms -> ms.getMatchId().equals(matchId));
		if (matchSession == null)
			matchSession = new MatchSession(matchId, new KSet<>());
		matchSession.getSessions().add(session);
	}

	@Override
	public void handleMessage(WebSocketSession session, WebSocketMessage<?> message) throws Exception {
		if (message instanceof MatchMessage) {
			var matchMessage = (MatchMessage) message;
			var player = playerService.findPlayerById(matchMessage.getPlayerId());
			var match = matchService.findById(matchMessage.getMatchId());

			var isSuccessed = false;
			switch (matchMessage.getMatchMessageType()) {
				case Join -> isSuccessed = matchService.join(match, player);
				case Leave -> isSuccessed = matchService.leave(match, player);
				case Start -> isSuccessed = matchService.start(match, player);
				case Cancel -> isSuccessed = matchService.cancel(match, player);
			}

			if (isSuccessed) {
				// TODO: MatchServiceにメッセージを送るようのメソッドを追加予定
			}
		}
	}

	@Override
	public void afterConnectionClosed(WebSocketSession session, CloseStatus closeStatus) throws Exception {
		var matchId = session.getHandshakeHeaders().getFirst("Match-Id");
		var matchSession = matchSessions.selectOne(ms -> ms.getMatchId().equals(matchId));
		matchSession.getSessions().remove(session);
	}
}
