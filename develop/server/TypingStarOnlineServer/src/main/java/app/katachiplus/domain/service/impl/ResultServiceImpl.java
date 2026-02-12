package app.katachiplus.domain.service.impl;

import org.springframework.stereotype.Service;

import app.katachiplus.domain.model.GameResult;
import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.model.MatchLogic;
import app.katachiplus.domain.model.Player;
import app.katachiplus.domain.service.ResultService;

@Service
public class ResultServiceImpl implements ResultService {
	
	@Override
	public boolean postGameResult(Match match, Player player, GameResult gameResult) {
		return MatchLogic.setResultToPlayer(match, player, gameResult);
	}
}
