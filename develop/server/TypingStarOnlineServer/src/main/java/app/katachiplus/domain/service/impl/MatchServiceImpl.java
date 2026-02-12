package app.katachiplus.domain.service.impl;

import org.springframework.scheduling.annotation.EnableScheduling;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.model.MatchLogic;
import app.katachiplus.domain.model.MatchState;
import app.katachiplus.domain.model.Player;
import app.katachiplus.domain.service.MatchService;
import app.katachiplus.utility.KSet;

@Service
@EnableScheduling
public class MatchServiceImpl implements MatchService {

	private final KSet<Match> matches = new KSet<>();

	@Override
	public KSet<Match> findAll() {
		return matches;
	}

	@Override
	public Match findById(String matchId) {
		return matches.selectOne(m -> m.getId().equals(matchId));
	}

	@Override
	public boolean join(Match match, Player player) {
		return MatchLogic.addPlayer(match, player);
	}

	@Override
	public boolean leave(Match match, Player player) {
		return MatchLogic.removePlayer(match, player);
	}

	@Override
	public boolean start(Match match, Player player) {
		return MatchLogic.startMatch(match, player);
	}

	@Override
	public boolean cancel(Match match, Player player) {
		return MatchLogic.cancelMatch(match, player);
	}

	@Override
	@Scheduled(fixedRate = 5000)
	public void invalidate() {
		matches
				.selectMany(m -> (m.getState() == MatchState.Canceled) ||
						(m.getState() == MatchState.Ended))
				.forEach(m -> matches.remove(m));
	}
}
