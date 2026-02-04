package app.katachiplus.domain.model;

import app.katachiplus.utility.KSet;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Match {
	private String matchId;
	private MatchState state;
	private KSet<Player> players;

	public Match(String matchId) {
		this(
				matchId,
				MatchState.Waiting,
				new KSet<Player>());
	}
	
	public Match(String matchId, MatchState state) {
		this(
				matchId,
				state,
				new KSet<Player>());
	}

	public void start(Player connectedPlayer) {
		MatchLogic.startMatch(this, connectedPlayer);
	}

	public boolean addPlayer(Player newPlayer) {
		return MatchLogic.addPlayer(this, newPlayer);
	}

	public boolean removePlayer(Player player) {
		return MatchLogic.removePlayer(this, player);
	}
	
	public void setResultToPlayer(Player player, GameResult gameResult) {
		MatchLogic.setResultToPlayer(this, player, gameResult);
	}

	@Override
	public boolean equals(Object other) {
		if (this == other)
			return true;
		if (other instanceof Match)
			return this.matchId == ((Match) other).getMatchId();
		return false;
	}

	@Override
	public int hashCode() {
		return super.hashCode();
	}
}
