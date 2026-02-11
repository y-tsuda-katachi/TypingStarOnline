package app.katachiplus.domain.model.message;

import lombok.Data;

@Data
public class ProgressMessage {
	private Integer playerId;
	private Integer progress;
}
