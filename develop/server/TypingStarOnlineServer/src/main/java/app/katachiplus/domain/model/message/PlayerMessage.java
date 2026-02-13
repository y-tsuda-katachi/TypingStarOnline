package app.katachiplus.domain.model.message;

import lombok.Data;

@Data
public class PlayerMessage {
	private PlayerMessageType type;
	private String playerId;
	private String password;
}
