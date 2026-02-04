package app.katachiplus.domain.service.impl;

import org.springframework.scheduling.annotation.EnableScheduling;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;
import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;

import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.model.MatchLogic;
import app.katachiplus.domain.model.MatchState;
import app.katachiplus.domain.model.Player;
import app.katachiplus.domain.service.MatchService;
import app.katachiplus.utility.KSet;
import lombok.extern.slf4j.Slf4j;

@Service
@EnableScheduling
@Slf4j
public class MatchServiceImpl implements MatchService {

	private final KSet<Match> matches = new KSet<>() {
		{
			add(new Match("001"));
			add(new Match("002"));
			add(new Match("003"));
			add(new Match("004"));
			add(new Match("005", MatchState.Started));
		}
	};

	@Override
	public KSet<Match> findMatches() {
		var matchList = matches;
		log.info("Founded : " + matchList);
		return matchList;
	}

	@Override
	public Match findMatchById(String matchId) {
		var match = matches.selectOne(m -> m.getMatchId().equals(matchId));
		log.info("Found:" + match);
		return match;
	}

	@Override
	public SseEmitter enterMatch(Player player, Match match) {
		var isAdded = match.addPlayer(player);
		log.info(player.getPlayerName() + (isAdded ? " entered to the match" : " couldn't enter to the match")
				+ match.getMatchId());
		return player.getEmitter();
	}

	@Override
	public boolean exitMatch(Player player, Match match) {
		var isRemoved = match.removePlayer(player);
		log.info(player + (isRemoved ? " exits from " : " couldn't exit from ") + match);
		return isRemoved;
	}

	@Override
	public boolean startMatch(Player player, Match match) {
		match.start(player);
		var isStarted = (match.getState() == MatchState.Started);
		log.info(player + (isStarted ? " started " : " couldn't start ") + match);
		return isStarted;
	}

	@Override
	@Scheduled(fixedRate = 5000)
	public void invalidate() {
		matches.selectMany(m -> (m.getState() == MatchState.Ended))
				.forEach(m -> MatchLogic.initialize(m));
	}
}
