package app.katachiplus.domain.service;

import app.katachiplus.domain.model.Invalidatable;
import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.model.Player;
import app.katachiplus.utility.KSet;

public interface MatchService extends Invalidatable {
	public KSet<Match> findAll();
	public Match findById(String matchId);
	public boolean join(Match match, Player player);
	public boolean leave(Match match, Player player);
	public boolean start(Match match, Player player);
	public boolean cancel(Match match, Player player);
	public void invalidate();
}
