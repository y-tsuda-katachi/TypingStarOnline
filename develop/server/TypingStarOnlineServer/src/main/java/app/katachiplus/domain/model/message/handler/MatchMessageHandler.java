package app.katachiplus.domain.model.message.handler;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.WebSocketMessage;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.AbstractWebSocketHandler;

import app.katachiplus.domain.model.match.MatchSession;
import app.katachiplus.domain.model.message.MatchMessage;
import app.katachiplus.domain.model.message.MatchProgressMessage;
import app.katachiplus.domain.model.message.MatchResultMessage;
import app.katachiplus.domain.service.MatchService;
import app.katachiplus.domain.service.PlayerService;
import app.katachiplus.domain.service.ResultService;
import app.katachiplus.utility.KSet;
import lombok.extern.slf4j.Slf4j;

@Component
@Slf4j
public class MatchMessageHandler extends AbstractWebSocketHandler {

	private final KSet<MatchSession> matchSessions = new KSet<>();
	@Autowired
	private PlayerService playerService;
	@Autowired
	private MatchService matchService;
	@Autowired
	private ResultService resultService;

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
			var player = playerService.findById(matchMessage.getPlayerId());
			var match = matchService.findById(matchMessage.getMatchId());
			var isSucceeded = false;

			if (message instanceof MatchProgressMessage)
				isSucceeded = true; // とりあえず無条件で通す
			else if (message instanceof MatchResultMessage)
				isSucceeded = resultService.postGameResult(match, player,
						((MatchResultMessage) message).getGameResult());
			else
				switch (matchMessage.getType()) {
					case Join -> isSucceeded = matchService.join(match, player);
					case Leave -> isSucceeded = matchService.leave(match, player);
					case Start -> isSucceeded = matchService.start(match, player);
					case Cancel -> isSucceeded = matchService.cancel(match, player);
				}

			if (isSucceeded == false)
				// 何らかの理由で処理に失敗したら送信しない
				return;

			matchSessions
					.selectOne(ms -> ms
							.getMatchId()
							.equals(match.getId()))
					.getSessions()
					.forEach(s -> {
						try {
							s.sendMessage(message);
						} catch (Exception e) {
							log.error(e.getMessage());
						}
					});
		}
	}

	@Override
	public void afterConnectionClosed(WebSocketSession session, CloseStatus closeStatus) throws Exception {
		var matchId = session.getHandshakeHeaders().getFirst("Match-Id");
		var matchSession = matchSessions.selectOne(ms -> ms.getMatchId().equals(matchId));
		matchSession.getSessions().remove(session);
	}
}
