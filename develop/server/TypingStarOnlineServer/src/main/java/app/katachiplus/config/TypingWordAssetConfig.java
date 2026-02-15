package app.katachiplus.config;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.stream.Stream;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import app.katachiplus.domain.model.TypingWord;
import app.katachiplus.domain.model.TypingWordAsset;
import app.katachiplus.utility.KSet;
import lombok.extern.slf4j.Slf4j;

@Configuration
@Slf4j
public class TypingWordAssetConfig {
	
	@Value("${assets.path}")
	private String assetsDirectoryPath;
	
	@Bean
	KSet<TypingWordAsset> typingWordAssets() throws IOException {		
		log.info("Start loading from " + assetsDirectoryPath);
		var typingWordAssets = new KSet<TypingWordAsset>();
		try (Stream<Path> paths = Files.list(Path.of(assetsDirectoryPath))) {
			paths.forEach(path -> {				
				var assetName = path.getFileName().toString().replace(".csv", "");
				try {
					var typingWords = Files.lines(path)
							.skip(0)
							.map(TypingWord::fromCSV);
					typingWordAssets.add(new TypingWordAsset(assetName, typingWords.toList()));
				} catch (IOException e) {
					log.error(e.getMessage());
				}
			});
		}
		for (var asset : typingWordAssets) {
			log.info(asset.getAssetName() + " : " + asset.getTypingWords());
		}
		log.info("Loaded :" + typingWordAssets.size());
		return typingWordAssets;
	}
}
