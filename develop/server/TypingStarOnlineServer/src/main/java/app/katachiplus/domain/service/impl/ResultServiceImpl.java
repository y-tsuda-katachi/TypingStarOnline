package app.katachiplus.domain.service.impl;

import org.springframework.stereotype.Service;

import app.katachiplus.domain.model.GameResult;
import app.katachiplus.domain.model.match.Match;
import app.katachiplus.domain.model.match.MatchLogic;
import app.katachiplus.domain.model.player.Player;
import app.katachiplus.domain.service.ResultService;

@Service
public class ResultServiceImpl implements ResultService {
	
	@Override
	public boolean postGameResult(Match match, Player player, GameResult gameResult) {
		return MatchLogic.setResultToPlayer(match, player, gameResult);
	}
}
