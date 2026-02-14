package app.katachiplus.domain.model.player;

import java.util.Comparator;

public class PlayerComparator implements Comparator<Player> {

	@Override
	public int compare(Player o1, Player o2) {
		return Float.compare(
				o1.getGameResult().getElapsedMilliSeconds(),
				((Player) o2).getGameResult().getElapsedMilliSeconds());
	}
}
