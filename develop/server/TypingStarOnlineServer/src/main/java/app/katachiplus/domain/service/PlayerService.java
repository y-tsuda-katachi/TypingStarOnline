package app.katachiplus.domain.service;

import app.katachiplus.domain.model.Invalidatable;
import app.katachiplus.domain.model.player.Player;

public interface PlayerService extends Invalidatable {
	public boolean signup(String playerId, String password);
	public boolean logout(String playerId);
	public Player findById(String playerId);
	public void updateLastAccessedTime(Player player, Long lastAccessedTime);
	public void invalidate();
}
