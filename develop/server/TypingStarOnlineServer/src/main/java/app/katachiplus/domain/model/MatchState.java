package app.katachiplus.domain.model;

/**
 * マッチの状態
 * */
public enum MatchState {
	/** 接続待ち */
	Waiting(0),
	/** 開始済 */
	Started(1),
	/** 終了済 */
	Ended(1);
	
	private Integer value;
	
	private MatchState(Integer value) {
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
