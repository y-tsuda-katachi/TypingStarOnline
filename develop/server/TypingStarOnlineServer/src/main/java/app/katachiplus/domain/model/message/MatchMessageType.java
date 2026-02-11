package app.katachiplus.domain.model.message;

/**
 * マッチメッセージ種類
 * */
public enum MatchMessageType {
	
	/** マッチへ参加 */
	Join(0),
	/** マッチを離脱 */
	Leave(1),
	/** マッチを開始 */
	Start(2),
	/** マッチを作成 */
	Create(3),
	/** マッチ削除 */
	Destroy(4);
	
	private int value;
	
	private MatchMessageType(int value) {
		this.value = value;
	}
	
	public int getValue() {
		return this.value;
	}
}
