package app.katachiplus.domain.model.player;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class PlayerResponse {
	private Player player;
	private String jwtToken;
}
