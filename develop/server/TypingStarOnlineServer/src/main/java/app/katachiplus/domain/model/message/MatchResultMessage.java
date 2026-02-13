package app.katachiplus.domain.model.message;

import app.katachiplus.domain.model.GameResult;
import lombok.Data;
import lombok.EqualsAndHashCode;

@Data
@EqualsAndHashCode(callSuper=false)
public class MatchResultMessage extends MatchMessage {
	private GameResult gameResult;
}
