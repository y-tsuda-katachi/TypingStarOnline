package app.katachiplus.domain.model.message;

import lombok.Data;
import lombok.EqualsAndHashCode;

@Data
@EqualsAndHashCode(callSuper=false)
public class MatchProgressMessage extends MatchMessage {
	private Integer progress;
}
