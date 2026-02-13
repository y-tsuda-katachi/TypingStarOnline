package app.katachiplus.domain.service.impl;

import java.time.Instant;

import org.springframework.scheduling.annotation.EnableScheduling;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import app.katachiplus.domain.model.Player;
import app.katachiplus.domain.service.PlayerService;
import app.katachiplus.utility.KSet;

@Service
@EnableScheduling
public class PlayerServiceImpl implements PlayerService {

	private final KSet<Player> players = new KSet<>();

	@Override
	public boolean addPlayer(Player player) {
		if (players.any(p -> p.equals(player)))
			players.selectOne(p -> p.equals(player))
					.setName(player.getName());
		return players.add(player);
	}

	@Override
	public Player findById(String playerId) {
		return players.selectOne(p -> p.getId().equals(playerId));
	}

	@Override
	public boolean removeById(String playerId) {
		var player = players.selectOne(p -> p.getId().equals(playerId));
		return players.remove(player);
	}

	@Override
	public void updateLastAccessedTime(Player player, Long lastAccessedTime) {
		player.setLastAccessedTime(lastAccessedTime);
	}

	@Override
	@Scheduled(fixedRate = 5000)
	public void invalidate() {
		players
				.selectMany(p -> Instant.ofEpochMilli(p.getLastAccessedTime())
						.isBefore(Instant.now().minusSeconds(1800))) // デフォルトは30分でタイムアウトにしておく
				.forEach(p -> players.remove(p));
	}
}
