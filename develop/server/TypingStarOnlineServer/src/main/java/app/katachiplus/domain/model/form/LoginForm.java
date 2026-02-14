package app.katachiplus.domain.model.form;

import lombok.Data;

@Data
public class LoginForm {
	private String playerId;
	private String password;
}
