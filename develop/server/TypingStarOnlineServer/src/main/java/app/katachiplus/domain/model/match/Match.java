package app.katachiplus.domain.model.match;

import app.katachiplus.domain.model.TypingWordAsset;
import app.katachiplus.domain.model.player.Player;
import app.katachiplus.utility.KSet;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class Match {
	private String id;
	private Player owner;
	private TypingWordAsset typingWordAsset;
	private Integer maxPlayerAmount;
	private MatchState state;
	private KSet<Player> players;

	@Override
	public boolean equals(Object other) {
		if (other instanceof Match)
			return this.id == ((Match) other).getId();
		return super.equals(other);
	}

	@Override
	public int hashCode() {
		return super.hashCode();
	}
}
