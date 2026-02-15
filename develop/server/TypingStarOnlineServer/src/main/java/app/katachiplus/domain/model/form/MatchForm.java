package app.katachiplus.domain.model.form;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class MatchForm {
	private String assetName;
	private Integer maxPlayerAmount;
}
