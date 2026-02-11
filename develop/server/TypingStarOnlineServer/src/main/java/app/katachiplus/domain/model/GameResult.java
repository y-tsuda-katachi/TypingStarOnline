package app.katachiplus.domain.model;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class GameResult {
	private Integer rank; // 順位
	private Float elapsedMilliSeconds; // 経過時間（ミリ秒）
	private Integer failureCount; // 間違えた回数
}