package app.katachiplus.filter;

import java.io.IOException;
import java.time.Instant;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import app.katachiplus.domain.service.PlayerService;
import app.katachiplus.utility.KSet;

@Component
public class PlayerFilter extends OncePerRequestFilter {

	@Autowired
	private PlayerService playerService;

	private final KSet<String> shouldNotFilterPatterns = new KSet<>() {
		{
			add("/actuator/health");	// ヘルスチェック時
			add("/player/connect");		// 初回接続時
			add("/match/message");		// マッチ中のWS通信
		}
	};

	@Override
	protected boolean shouldNotFilter(HttpServletRequest request) throws ServletException {
		return shouldNotFilterPatterns.any(p -> request.getRequestURI().startsWith(p));
	}

	@Override
	protected void doFilterInternal(HttpServletRequest request, HttpServletResponse response, FilterChain filterChain)
			throws ServletException, IOException {
		var playerId = request.getParameter("playerId");
		var player = playerService.findById(playerId);

		if (player != null) {
			playerService.updateLastAccessedTime(player, Instant.now().toEpochMilli());
			filterChain.doFilter(request, response);
		}
	}

}
