package app.katachiplus.domain.model.message;

/**
 * プレイヤーメッセージ種類
 * */
public enum PlayerMessageType {
	/**
	 * 接続
	 * */
	Connect(0),
	/**
	 * 切断
	 * */
	Disconnect(1);
	
	private Integer value;
	
	private PlayerMessageType(Integer value) {
		this.value = value;
	}
	
	public Integer getValue() {
		return this.value;
	}
	
	@Override
	public String toString() {
		return this.getValue().toString();
	}
}
