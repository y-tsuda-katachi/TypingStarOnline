package app.katachiplus.controller;

import java.time.Instant;

import jakarta.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import app.katachiplus.domain.model.Player;
import app.katachiplus.domain.service.PlayerService;

@RestController
@RequestMapping("/player")
public class PlayerController {

	@Autowired
	private PlayerService playerService;

	@GetMapping("/connect")
	public Player connect(@RequestParam String playerId, @RequestParam String playerName, HttpSession session) {
		if (playerId == null || playerId.isEmpty())
			playerId = session.getId();
		var player = new Player(playerId, playerName, Instant.now().toEpochMilli());
		return playerService.addPlayer(player) ? player : null;
	}
	
	@GetMapping("/disconnect")
	public boolean disconnect(@RequestParam String playerId) {
		return playerService.removeById(playerId);
	}
}
