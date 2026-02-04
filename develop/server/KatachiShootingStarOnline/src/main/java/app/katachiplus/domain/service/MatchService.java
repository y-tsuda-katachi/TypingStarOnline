package app.katachiplus.domain.service;

import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;

import app.katachiplus.domain.model.Invalidatable;
import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.model.Player;
import app.katachiplus.utility.KSet;

public interface MatchService extends Invalidatable {
	public KSet<Match> findMatches();
	public Match findMatchById(String matchId);
	public SseEmitter enterMatch(Player player, Match match);
	public boolean exitMatch(Player player, Match match);
	public boolean startMatch(Player player, Match match);
	public void invalidate();
}
