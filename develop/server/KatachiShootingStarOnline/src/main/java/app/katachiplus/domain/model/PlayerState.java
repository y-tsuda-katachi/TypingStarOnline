package app.katachiplus.domain.model;

/**
 * プレイヤーの状態
 * */
public enum PlayerState {
	/** 切断済 */
	Disconnected(-1),
	/** 接続済 */
	Connected(1),
	/** プレイ中 */
	InGame(2);
	
	private Integer value;
	
	private PlayerState(Integer value) {
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
