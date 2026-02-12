package app.katachiplus.domain.model;

import lombok.Data;

@Data
/**
 * タイピングワードをまとめたアセット
 * */
public class TypingWordAsset {
	private String assetName;
	private TypingWord[] typingWords;
}
