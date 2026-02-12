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
	/** マッチ中止 */
	Cancel(3);
	
	private Integer value;
	
	private MatchMessageType(Integer value) {
		this.value = value;
	}
	
	public Integer getValue() {
		return this.value;
	}
	
	@Override
	public String toString() {
		return getValue().toString();
	}
}
