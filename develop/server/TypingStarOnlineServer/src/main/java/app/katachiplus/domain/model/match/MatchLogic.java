package app.katachiplus.domain.model.match;

import java.util.List;

import app.katachiplus.domain.model.GameResult;
import app.katachiplus.domain.model.player.Player;
import app.katachiplus.domain.model.player.PlayerComparator;

public class MatchLogic {

	/**
	 * マッチにプレイヤーを追加する
	 * @return 追加できたかどうか
	 * */
	public static boolean addPlayer(Match match, Player player) {
		if (match.getState() != MatchState.Waiting)
			// マッチは待機中以外は参加できない
			return false;

		if (isPlayerInMatch(match, player))
			// 既にマッチ内のプレイヤーなので無視
			return false;

		return match.getPlayers().add(player);
	}

	/**
	 * マッチからプレイヤーを取り除く
	 * @return 取り除けたかどうか
	 * */
	public static boolean removePlayer(Match match, Player player) {
		if (isPlayerInMatch(match, player) == false)
			// マッチに参加していないので削除しない
			return false;

		return match.getPlayers().remove(player);
	}

	/**
	 * マッチを開始する
	 * @return 開始できたかどうか
	 * */
	public static boolean startMatch(Match match, Player player) {
		if (match.getState() != MatchState.Waiting)
			// 接続待ち以外はスタートできない
			return false;

		if (isPlayerInMatch(match, player) == false)
			// マッチ内のプレイヤーじゃないから無視
			return false;
		
		if (isOwnerPlayer(match, player) == false)
			// マッチの作成者じゃないから無視
			return false;

		match.setState(MatchState.Started);

		return (match.getState() == MatchState.Started);
	}

	/**
	 * マッチをキャンセルする
	 * @return キャンセルできたかどうか
	 * */
	public static boolean cancelMatch(Match match, Player player) {
		if (match.getState() != MatchState.Waiting)
			// 接続待ち以外はキャンセルできない
			return false;
		
		if (isPlayerInMatch(match, player) == false)
			// マッチに参加していないので無視
			return false;
		
		if (isOwnerPlayer(match, player) == false)
			// マッチの作成者じゃないから無視
			return false;

		match.setState(MatchState.Canceled);

		return (match.getState() == MatchState.Canceled);
	}

	/**
	 * プレイヤーにリザルトをセットし
	 * ランキングを更新する
	 * */
	public static boolean setResultToPlayer(Match match, Player player,
			GameResult gameResult) {
		if (match.getState() != MatchState.Started)
			// 開始済みじゃないと結果をセットできない
			return false;
		
		if (isPlayerInMatch(match, player) == false)
			// マッチに参加していないので無視
			return false;

		player.setGameResult(gameResult);
		player.getGameResult().setRank(getPlayerRank(match, player));

		if (hasAllPlayersGameResult(match))
			match.setState(MatchState.Ended);
		
		return player.hasGameResult();
	}

	private static boolean isPlayerInMatch(Match match, Player player) {
		return match.getPlayers().contains(player);
	}
	
	private static boolean isOwnerPlayer(Match match, Player player) {
		return match.getOwner().equals(player);
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
		return match
				.getPlayers()
				.stream()
				.filter(p -> p.hasGameResult())
				.sorted(new PlayerComparator())
				.toList();
	}
}
