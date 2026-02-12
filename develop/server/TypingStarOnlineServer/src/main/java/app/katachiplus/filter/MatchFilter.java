package app.katachiplus.filter;

import java.io.IOException;

import jakarta.servlet.FilterChain;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import app.katachiplus.domain.service.MatchService;
import app.katachiplus.utility.KSet;

@Component
public class MatchFilter extends OncePerRequestFilter {

	@Autowired
	private MatchService matchService;

	private final KSet<String> shouldNotFilterPatterns = new KSet<>() {
		{
			add("/actuator/health");	// ヘルスチェック時
			add("/player");				// プレイヤー機能
			add("/match/get/all");		// 未接続時
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
		var matchId = request.getParameter("matchId");
		if (matchId != null) {
			var match = matchService.findById(matchId);
			
			if (match != null)
				filterChain.doFilter(request, response);
		}
	}

}
