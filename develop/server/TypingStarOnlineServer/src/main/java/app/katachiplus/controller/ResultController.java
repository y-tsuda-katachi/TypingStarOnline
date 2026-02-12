package app.katachiplus.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import app.katachiplus.domain.model.GameResult;
import app.katachiplus.domain.service.MatchService;
import app.katachiplus.domain.service.PlayerService;
import app.katachiplus.domain.service.ResultService;

@RestController
@RequestMapping("/result")
public class ResultController {

	@Autowired
	private PlayerService playerService;

	@Autowired
	private MatchService matchService;

	@Autowired
	private ResultService resultService;

	@PostMapping("/post")
	public boolean post(@RequestBody GameResult gameResult, @RequestParam String playerId,
			@RequestParam String matchId) {
		var player = playerService.findPlayerById(playerId);
		var match = matchService.findById(matchId);
		return resultService.postGameResult(match, player, gameResult);
	}
}
