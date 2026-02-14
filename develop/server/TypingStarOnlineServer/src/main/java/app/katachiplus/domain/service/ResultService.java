package app.katachiplus.domain.service;

import app.katachiplus.domain.model.GameResult;
import app.katachiplus.domain.model.match.Match;
import app.katachiplus.domain.model.player.Player;

public interface ResultService {
	public boolean postGameResult(Match match, Player player, GameResult gameResult);
}
