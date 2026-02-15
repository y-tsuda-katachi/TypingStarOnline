package app.katachiplus.domain.model;

import java.util.List;

import lombok.AllArgsConstructor;
import lombok.Data;

/**
 * タイピングワードをまとめたアセット
 * */
@Data
@AllArgsConstructor
public class TypingWordAsset {
	private String assetName;
	private List<TypingWord> typingWords;
}
