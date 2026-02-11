package app.katachiplus.domain.model;

import java.io.IOException;
import java.util.List;

import org.springframework.web.servlet.mvc.method.annotation.SseEmitter;

public class MatchLogic {
	
	public static void initialize(Match match) {
		match.getPlayers().clear();
		match.setState(MatchState.Waiting);
	}
	
	public static void startMatch(Match match, Player connectedPlayer) {
		if (match.getState() != MatchState.Waiting)
			// 接続待ち以外はスタートできない
			return;
		
		if (isPlayerInMatch(match, connectedPlayer) == false)
			// マッチ内のプレイヤーじゃないから無視
			return;

		
		match.getPlayers().forEach(player -> {
			if (player.hasEmitter()) {
				var emitter = player.getEmitter();
				try {
					emitter.send(SseEmitter.event().name("Game Start"));
					emitter.complete();
					player.setGameResult(null);
					player.setState(PlayerState.InGame);
				} catch (IOException ex) {
					emitter.completeWithError(ex);
					// 不正な操作があったので切断する
					player.setState(PlayerState.Disconnected);
				}
			}
			else
				// 不正な操作があったので切断する
				player.setState(PlayerState.Disconnected);
		});
		
		match.setState(MatchState.Started);
	}
	
	public static boolean addPlayer(Match match, Player newPlayer) {
		if (match.getState() != MatchState.Waiting)
			// マッチは待機中以外は参加できない
			return false;
		
		if (isPlayerInMatch(match, newPlayer))
			// 既にマッチ内のプレイヤーなので無視
			return false;
		
		if (newPlayer.getState() != PlayerState.Connected)
			// プレイヤーは接続済み以外は参加できない
			return false;
		
		var isAdded = match.getPlayers().add(newPlayer);
		if (isAdded)
			newPlayer.setEmitter(new SseEmitter(0L));
		return isAdded;
	}
	
	public static boolean removePlayer(Match match, Player player) {
		if (isPlayerInMatch(match, player) == false)
			// マッチに参加していないので削除しない
			return false;
		
		var isRemoved = match.getPlayers().remove(player);
		if (isRemoved) {
			player.setEmitter(null);
			player.setState(PlayerState.Connected);
		}
		return isRemoved;
	}
	
	public static void setResultToPlayer(Match match, Player player, 
			GameResult gameResult) {
		if (isPlayerInMatch(match, player) == false)
			// マッチに参加していないので無視
			return;
		
		player.setGameResult(gameResult);
		player.getGameResult().setRank(getPlayerRank(match, player));
		player.setState(PlayerState.Connected);
		
		if (hasAllPlayersGameResult(match))
			match.setState(MatchState.Ended);
	}
	
	private static boolean isPlayerInMatch(Match match, Player player) {
		return match.getPlayers().contains(player);
	}
	
	private static boolean hasAllPlayersGameResult(Match match) {
		return match.getPlayers().all(p -> p.hasGameResult());
	}
	
	private static int getPlayerRank(Match match, Player player) {
		var rankings = getPlayerRankings(match);
		var rank = rankings.indexOf(player);
		return (rank + 1); // min 1
	}
	
	private static List<Player> getPlayerRankings(Match match) {
		return match.getPlayers()
				.stream()
				.filter(p -> p.hasGameResult())
				.sorted()
				.toList();
	}
}
