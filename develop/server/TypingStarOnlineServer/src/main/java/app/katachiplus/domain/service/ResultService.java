package app.katachiplus.domain.service;

import app.katachiplus.domain.model.GameResult;
import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.model.Player;

public interface ResultService {
	public boolean postGameResult(Match match, Player player, GameResult gameResult);
}
