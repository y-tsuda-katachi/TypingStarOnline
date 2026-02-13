package app.katachiplus.domain.service;

import app.katachiplus.domain.model.Invalidatable;
import app.katachiplus.domain.model.Player;

public interface PlayerService extends Invalidatable {
	public boolean addPlayer(Player player);
	public Player findById(String playerId);
	public boolean removeById(String playerId);
	public void updateLastAccessedTime(Player player, Long lastAccessedTime);
	public void invalidate();
}
