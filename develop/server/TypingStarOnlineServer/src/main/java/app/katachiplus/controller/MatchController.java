package app.katachiplus.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import app.katachiplus.domain.model.Match;
import app.katachiplus.domain.service.MatchService;
import app.katachiplus.utility.KSet;

@RestController
@RequestMapping("/match")
public class MatchController {

	/*@Autowired
	private PlayerService playerService;*/

	@Autowired
	private MatchService matchService;

	@GetMapping("/get/all")
	public KSet<Match> getMatches() {
		return matchService.findAll();
	}
	
	@GetMapping("/get")
	public Match getMatch(@RequestParam String matchId) {
		return matchService.findById(matchId);
	}
	
	/*@GetMapping("/enter")
	public Object enter(@RequestParam String playerId, @RequestParam String matchId) {
		var player = playerService.findPlayerById(playerId);
		var match = matchService.findMatchById(matchId);
		return matchService.enterMatch(player, match);
	}
	
	@GetMapping("/exit")
	public boolean exit(@RequestParam String playerId, @RequestParam String matchId) {
		var player = playerService.findPlayerById(playerId);
		var match = matchService.findMatchById(matchId);
		return matchService.exitMatch(player, match);
	}
	
	@GetMapping("/start")
	public boolean start(@RequestParam String playerId, @RequestParam String matchId) {
		var player = playerService.findPlayerById(playerId);
		var match = matchService.findMatchById(matchId);
		return matchService.startMatch(player, match);
	}*/
}
