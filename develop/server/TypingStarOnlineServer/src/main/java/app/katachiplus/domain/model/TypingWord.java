package app.katachiplus.domain.model;

import lombok.AllArgsConstructor;
import lombok.Data;

/**
 * タイピングワード
 * */
@Data
@AllArgsConstructor
public class TypingWord {
	private String label;
	private String roman;
	
	public static TypingWord fromCSV(String line) {
		var word = line.split(",");
		return new TypingWord(word[0], word[1]);
	}
}
