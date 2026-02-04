package app.katachiplus.domain.service.impl;

import java.time.Instant;

import org.springframework.scheduling.annotation.EnableScheduling;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import app.katachiplus.domain.model.Player;
import app.katachiplus.domain.model.PlayerState;
import app.katachiplus.domain.service.PlayerService;
import app.katachiplus.utility.KSet;
import lombok.extern.slf4j.Slf4j;

@Service
@EnableScheduling
@Slf4j
public class PlayerServiceImpl implements PlayerService {

	private final KSet<Player> players = new KSet<Player>();

	@Override
	public boolean addPlayer(Player player) {
		var isAdded = players.add(player);
		if (isAdded)
			player.setState(PlayerState.Connected);
		else // 名前だけ変更する
			players.selectOne(p -> p
					.equals(player))
			.setPlayerName(player
					.getPlayerName());
		log.info(player.getPlayerName() + (isAdded ? " sucessed connection" : " failed connection"));
		return isAdded;
	}

	@Override
	public Player findPlayerById(String playerId) {
		var player = players.selectOne(p -> p.getPlayerId().equals(playerId));
		log.info(playerId + (player != null ? " is founded on server" : " is not founded on server"));
		return player;
	}

	@Override
	public boolean removePlayerById(String playerId) {
		var player = players.selectOne(p -> p.getPlayerId().equals(playerId));
		var isRemoved = players.remove(player);
		log.info(player.getPlayerName() + (isRemoved ? " successed disconnection" : " failed disconnection"));
		return isRemoved;
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
						.isBefore(Instant.now()
								.minusSeconds(1800))) // デフォルトは30分でタイムアウトにしておく
				.forEach(p -> p.setState(PlayerState.Disconnected));

		players
				.removeAll(players
						.selectMany(p -> (p.getState() == PlayerState.Disconnected)));
	}
}
