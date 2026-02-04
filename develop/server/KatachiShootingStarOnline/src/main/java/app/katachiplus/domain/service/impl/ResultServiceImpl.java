package app.katachiplus.domain.service.impl;

import org.springframework.stereotype.Service;

import app.katachiplus.domain.model.GameResult;
import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.model.Player;
import app.katachiplus.domain.service.ResultService;
import lombok.extern.slf4j.Slf4j;

@Service
@Slf4j
public class ResultServiceImpl implements ResultService {
	
	@Override
	public boolean postGameResult(Player player, Match match, GameResult gameResult) {
		match.setResultToPlayer(player, gameResult);
		var isSet = player.getGameResult() != null;
		log.info(player + (isSet ? " puts " : " couldn't put ") + gameResult);
		return isSet;
	}
}
