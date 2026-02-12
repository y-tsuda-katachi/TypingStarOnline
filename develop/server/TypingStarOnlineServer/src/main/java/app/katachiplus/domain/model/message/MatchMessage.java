package app.katachiplus.domain.model.message;

import lombok.Data;

/**
 * マッチメッセージ
 * */
@Data
public class MatchMessage {
	private MatchMessageType matchMessageType;
	private String matchId;
	private String playerId;
}
