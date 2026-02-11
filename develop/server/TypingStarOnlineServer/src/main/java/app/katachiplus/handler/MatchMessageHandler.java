package app.katachiplus.handler;

import java.util.HashMap;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.WebSocketHandler;
import org.springframework.web.socket.WebSocketMessage;
import org.springframework.web.socket.WebSocketSession;

import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.service.MatchService;
import app.katachiplus.utility.KSet;
import lombok.extern.slf4j.Slf4j;

@Component
@Slf4j
public class MatchMessageHandler implements WebSocketHandler {
	
	private final Map<Match, KSet<WebSocketSession>> matchSessions = new HashMap<>();

	@Autowired
	private MatchService matchService;

	@Override
	public void afterConnectionEstablished(WebSocketSession session) throws Exception {
		var matchId = getMatchId(session);
		var match = matchService.findMatchById(matchId);
		var sessions = matchSessions.get(match);
		if (sessions == null) {
			sessions = new KSet<WebSocketSession>();
			sessions.add(session);
			matchSessions.put(match, sessions);
		} else {
			if (matchSessions
					.get(match)
					.any(s -> s.getId()
							.equals(session.getId())) == false)
				matchSessions
						.get(match)
						.add(session);
		}
	}

	@Override
	public void handleMessage(WebSocketSession session, WebSocketMessage<?> message) throws Exception {
		var matchId = getMatchId(session);
		var match = matchService.findMatchById(matchId);
		matchSessions
				.get(match)
				.selectMany(s -> s.getId().equals(session.getId()) == false)
				.forEach(s -> {
					try {
						s.sendMessage(message);
						log.info(session.getId() + " sends to " + s.getId());
					} catch (Exception e) {
						log.error(e.getMessage());
					}
				});
	}

	@Override
	public void afterConnectionClosed(WebSocketSession session, CloseStatus closeStatus) throws Exception {
		var matchId = getMatchId(session);
		var match = matchService.findMatchById(matchId);
		var player = matchSessions
				.get(match)
				.selectOne(s -> s.getId()
						.equals(session.getId()));
		matchSessions
				.get(match)
				.remove(player);
	}

	private String getMatchId(WebSocketSession session) throws Exception {
		return session
				.getHandshakeHeaders()
				.getFirst("Match-Id");
	}

	@Override
	public void handleTransportError(WebSocketSession session, Throwable exception) throws Exception {
	}

	@Override
	public boolean supportsPartialMessages() {
		return false;
	}
}
