package app.katachiplus.domain.model;

import lombok.Data;

@Data
public class GameResultMessage {
	private String playerId;
	private GameResult gameResult;
}
