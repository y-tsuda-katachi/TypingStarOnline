package app.katachiplus.utility;

import java.util.LinkedHashSet;
import java.util.function.Function;

/**
 * 検索機能を実装したLinkedHashSetクラス
 * @param E : 要素型
 * */
public class KSet<E> extends LinkedHashSet<E> {

	/**
	 * 条件に合う要素を一つだけ取得する
	 * @param condition - 条件
	 * @return 一致した要素
	 * */
	public E selectOne(Function<E, ?> condition) {
		for (var e : this)
			if ((boolean) condition.apply(e))
				return e;
		return null;
	}

	/**
	 * 条件に合う要素を複数取得する
	 * @param condition - 条件
	 * @return 一致した要素リスト
	 * */
	public KSet<E> selectMany(Function<E, ?> condition) {
		var l = new KSet<E>();
		for (var e : this)
			if ((boolean) condition.apply(e))
				l.add(e);
		return l;
	}
	
	/**
	 * 一つでも条件に合う要素があればTrue
	 * @param condition - 条件
	 * @return boolean
	 * */
	public boolean any(Function<E, ?> condition) {
		for (var e : this)
			if ((boolean) condition.apply(e))
				return true;
		return false;
	}
	
	/**
	 * 条件が全ての要素に合えばTrue
	 * @param condition - 条件
	 * @return boolean
	 * */
	public boolean all(Function<E, ?> condition) {
		for (var e : this)
			if ((boolean) condition.apply(e) == false)
				return false;
		return true;
	}
}